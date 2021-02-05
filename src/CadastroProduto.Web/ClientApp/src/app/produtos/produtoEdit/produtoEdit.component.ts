import { Component, OnInit } from '@angular/core';
import { ProdutoService } from 'src/app/_services/produto.service';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Produto } from 'src/app/_models/Produto';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse } from 'src/app/_models/ApiResponse';

@Component({
  selector: 'app-produto-edit',
  templateUrl: './produtoEdit.component.html',
  styleUrls: ['./produtoEdit.component.css']
})
export class ProdutoEditComponent implements OnInit {

  titulo = 'Editar Produto';
  produto: Produto = new Produto();
  imagemURL = 'assets/img/upload.png';
  registerForm: FormGroup;
  file: File;
  fileNameToUpdate: string;

  dataAtual = '';

  constructor(
    private produtoService: ProdutoService
    , private fb: FormBuilder
    , private router: ActivatedRoute
    , private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.validation();
    this.carregarProduto();
  }

  carregarProduto() {
    const idProduto = +this.router.snapshot.paramMap.get('id');
    this.produtoService.getProdutoById(idProduto).subscribe(
      (apiResponse: ApiResponse<Produto>) => {
        if (apiResponse && apiResponse.success){
          this.produto = apiResponse.data;
          this.fileNameToUpdate = apiResponse.data.imagemURL.toString();
          
          this.imagemURL = `http://localhost:5000/resources/images/${this.produto.imagemURL}?_ts=${this.dataAtual}`;
          
          this.produto.imagemURL = '';
          this.registerForm.patchValue(this.produto);
        }
        else this.toastrService.error(`Erro ao carregar Produto: ${apiResponse.errors[0]}`);
      }
    );
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [],
      nome: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      valor: ['', Validators.required],
      imagemURL: [],
      dataCriacao: [],
    });
  }

  onFileChange(produto: any, file: FileList) {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = produto.target.files;
    reader.readAsDataURL(file[0]);
  }

  salvarProduto() {
    this.produto = Object.assign({ id: this.produto.id }, this.registerForm.value);
    this.produto.imagemURL = this.fileNameToUpdate;

    this.uploadImagem();

    this.produtoService.putProduto(this.produto).subscribe(
      (apiResponse: ApiResponse<Produto>) => {
        if (apiResponse && apiResponse.success){
          this.toastrService.success("Editado com Sucesso!");
        }
        else this.toastrService.error(`Erro ao Editar: ${apiResponse.errors[0]}`);
      }, error => {
        this.toastrService.error(`Erro ao Editar: ${error}`);
      }
    );
  }

  uploadImagem() {
    if (this.registerForm.get('imagemURL').value !== '') {
      this.produtoService.postUpload(this.file, this.fileNameToUpdate)
        .subscribe(
          () => {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.imagemURL = `http://localhost:5000/resources/images/${this.produto.imagemURL}?_ts=${this.dataAtual}`;
          }
        );
    }
  }

}
