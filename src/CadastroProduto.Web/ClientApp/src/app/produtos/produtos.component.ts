import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../_services/produto.service';
import { Produto } from '../_models/Produto';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse } from '../_models/ApiResponse';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  titulo = 'Produtos';
  dataCriacao: string;
  nomeTemplate: string;
  produtosFiltrados: Produto[];
  produtos: Produto[];
  produto: Produto;
  modoSalvar = 'post';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarProduto = '';

  file: File;
  fileNameToUpdate: string;

  dataAtual: string;

  _filtroLista = '';

  constructor(
    private produtoService: ProdutoService
    , private fb: FormBuilder
    , private toastrService: ToastrService
  ) {
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.produtosFiltrados = this.filtroLista ? this.filtrarProdutos(this.filtroLista) : this.produtos;
  }

  editarProduto(produto: Produto, template: any) {
    this.nomeTemplate = 'Editar Produto';
    this.modoSalvar = 'put';
    this.openModal(template);
    this.produto = Object.assign({}, produto);
    this.fileNameToUpdate = produto.imagemURL.toString();
    this.produto.imagemURL = '';
    this.registerForm.patchValue(this.produto);
  }

  novoProduto(template: any) {
    this.nomeTemplate = 'Novo Produto';
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirProduto(produto: Produto, template: any) {
    this.nomeTemplate = 'Excluir Produto';
    this.openModal(template);
    this.produto = produto;
    this.bodyDeletarProduto = `Tem certeza que deseja excluir o Produto: ${produto.nome}, Código: ${produto.id}`;
  }

  getProdutos() {
    this.dataAtual = new Date().getMilliseconds().toString();

    this.produtoService.getAllProduto().subscribe(
      (apiResponse: ApiResponse<Produto[]>) => {
        if (apiResponse && apiResponse.success){
          this.produtos = apiResponse.data;
          this.produtosFiltrados = this.produtos;
          console.log(this.produtos);
        }
        else this.toastrService.error(`Erro ao carregar Produtos: ${apiResponse.errors[0]}`);
      }, error => {
        this.toastrService.error(`Erro ao tentar Carregar produtos: ${error}`);
      });
  }

  confirmeDelete(template: any) {
    this.produtoService.deleteProduto(this.produto.id).subscribe(
      (apiResponse: ApiResponse) => {
        if (apiResponse && apiResponse.success){
          template.hide();
          this.getProdutos();
          this.toastrService.error('Deletado com Sucesso');
        }
        else this.toastrService.error(`Erro ao Deletar: ${apiResponse.errors[0]}`);
      }, error => {
        this.toastrService.error('Erro ao tentar Deletar');
        console.log(error);
      }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getProdutos();
  }

  filtrarProdutos(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.produtos.filter(
      produto => produto.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
      valor: ['', Validators.required, Validators.min(0.01)],
      imagemURL: ['', Validators.required],
      dataCriacao: [this.dataCriacao],
    });
  }

  onFileChange(event) {
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
      console.log(this.file);
    }
  }

  uploadImagem() {
    if (this.modoSalvar === 'post') {
      const nomeArquivo = this.produto.imagemURL.split('\\', 3);
      this.produto.imagemURL = nomeArquivo[2];

      this.produtoService.postUpload(this.file, nomeArquivo[2])
        .subscribe(
          () => {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.getProdutos();
          }
        );
    } else {
      this.produto.imagemURL = this.fileNameToUpdate;
      this.produtoService.postUpload(this.file, this.fileNameToUpdate)
        .subscribe(
          () => {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.getProdutos();
          }
        );
    }
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.produto = Object.assign({}, this.registerForm.value);

        this.uploadImagem();

        this.produtoService.postProduto(this.produto).subscribe(
          (apiResponse: ApiResponse<Produto>) => {
            if (apiResponse && apiResponse.success){ 
              template.hide();
              this.getProdutos();
              this.toastrService.success('Inserido com Sucesso!');
            }
            else this.toastrService.error(`Erro ao Inserir: ${apiResponse.errors[0]}`);
          }, error => {
            this.toastrService.error(`Erro ao Inserir: ${error}`);
          }
        );
      } else {
        this.produto = Object.assign({ id: this.produto.id }, this.registerForm.value);

        this.uploadImagem();

        this.produtoService.putProduto(this.produto).subscribe(
          (apiResponse: ApiResponse<Produto>) => {
            if (apiResponse && apiResponse.success) {
              template.hide();
              this.getProdutos();
              this.toastrService.success('Editado com Sucesso!');
            }
            else this.toastrService.error(`Erro ao Editar: ${apiResponse.errors[0]}`);
          }, error => {
            this.toastrService.error(`Erro ao Editar: ${error}`);
          }
        );
      }
    }
  }

}
