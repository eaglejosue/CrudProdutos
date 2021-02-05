import { Component, OnInit } from '@angular/core';
import { ProdutoService } from 'src/app/_services/produto.service';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Produto } from 'src/app/_models/Produto';
import { ActivatedRoute } from '@angular/router';
import { BootstrapAlertService, BootstrapAlert } from 'ngx-bootstrap-alert';

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
    , public alertService: BootstrapAlertService
  ) {
  }

  ngOnInit() {
    this.validation();
    this.carregarProduto();
  }

  carregarProduto() {
    const idProduto = +this.router.snapshot.paramMap.get('id');
    this.produtoService.getProdutoById(idProduto)
      .subscribe(
        (produto: Produto) => {
          this.produto = Object.assign({}, produto);
          this.fileNameToUpdate = produto.imagemURL.toString();

          this.imagemURL = `http://localhost:5000/resources/images/${this.produto.imagemURL}?_ts=${this.dataAtual}`;

          this.produto.imagemURL = '';
          this.registerForm.patchValue(this.produto);
        }
      );
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataProduto: ['', Validators.required],
      imagemURL: [''],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
      redesSociais: this.fb.array([])
    });
  }

  criaLote(lote: any): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim]
    });
  }

  criaRedeSocial(redeSocial: any): FormGroup {
    return this.fb.group({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required]
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
      () => {
        this.alertService.alert(new BootstrapAlert("Editado com Sucesso!", "alert-success"));
      }, error => {
        this.alertService.alert(new BootstrapAlert(`Erro ao Editar: ${error}`, "alert-warning"));
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
