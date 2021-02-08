import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';
import { ApiResponse } from '../_models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseURL = 'https://localhost:5001/api/produto';

  constructor(private http: HttpClient) { }

  getAllProduto(): Observable<ApiResponse<Produto[]>> {
    return this.http.get<ApiResponse<Produto[]>>(this.baseURL);
  }

  getProdutoById(id: number): Observable<ApiResponse<Produto>> {
    return this.http.get<ApiResponse<Produto>>(`${this.baseURL}/${id}`);
  }

  postUpload(file: File, name: string) {
    let fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  postProduto(produto: Produto) {
    return this.http.post<ApiResponse<Produto>>(this.baseURL, produto);
  }

  putProduto(produto: Produto) {
    return this.http.put<ApiResponse<Produto>>(this.baseURL, produto);
  }

  deleteProduto(id: number) {
    return this.http.delete<ApiResponse>(`${this.baseURL}/${id}`);
  }

}
