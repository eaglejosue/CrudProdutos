import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProdutoService } from '../_services/produto.service';
import { Produto } from '../_models/Produto';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BootstrapAlertService, BootstrapAlert } from 'ngx-bootstrap-alert';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  titulo = 'Produtos';
  dataProduto: string;
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
    , public alertService: BootstrapAlertService
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
    this.modoSalvar = 'put';
    this.openModal(template);
    this.produto = Object.assign({}, produto);
    this.fileNameToUpdate = produto.imagemURL.toString();
    this.produto.imagemURL = '';
    this.registerForm.patchValue(this.produto);
  }

  novoProduto(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirProduto(produto: Produto, template: any) {
    this.openModal(template);
    this.produto = produto;
    this.bodyDeletarProduto = `Tem certeza que deseja excluir o Produto: ${produto.nome}, Código: ${produto.id}`;
  }

  confirmeDelete(template: any) {
    this.produtoService.deleteProduto(this.produto.id).subscribe(
      () => {
        template.hide();
        this.getProdutos();
        this.alertService.alert(new BootstrapAlert('Deletado com Sucesso', "alert-success"));
      }, error => {
        this.alertService.alert(new BootstrapAlert('Erro ao tentar Deletar', "alert-warning"));
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
      dataProduto: ['', Validators.required],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onFileChange(event) {
    const reader = new FileReader();

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
          (novoProduto: Produto) => {
            template.hide();
            this.getProdutos();
            this.alertService.alert(new BootstrapAlert('Inserido com Sucesso!', 'alert-warning'));
          }, error => {
            this.alertService.alert(new BootstrapAlert(`Erro ao Inserir: ${error}`, 'alert-warning'));
          }
        );
      } else {
        this.produto = Object.assign({ id: this.produto.id }, this.registerForm.value);

        this.uploadImagem();

        this.produtoService.putProduto(this.produto).subscribe(
          () => {
            template.hide();
            this.getProdutos();
            this.alertService.alert(new BootstrapAlert('Editado com Sucesso!', 'alert-success'));
          }, error => {
            this.alertService.alert(new BootstrapAlert(`Erro ao Editar: ${error}`, 'alert-warning'));
          }
        );
      }
    }
  }

  getProdutos() {
    this.dataAtual = new Date().getMilliseconds().toString();

    this.produtoService.getAllProduto().subscribe(
      (_produtos: Produto[]) => {
        this.produtos = _produtos;
        this.produtosFiltrados = this.produtos;
        console.log(this.produtos);
      }, error => {
        this.alertService.alert(new BootstrapAlert(`Erro ao tentar Carregar produtos: ${error}`, 'alert-warning'));
      });
  }

}