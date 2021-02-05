import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';
import { ApiResponse } from '../_models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseURL = 'http://localhost:5000/api/produto';

  constructor(private http: HttpClient) { }

  getAllProduto(): Observable<ApiResponse<Produto[]>> {
    return this.http.get<ApiResponse<Produto[]>>(this.baseURL);
  }

  getProdutoById(id: number): Observable<ApiResponse<Produto>> {
    return this.http.get<ApiResponse<Produto>>(`${this.baseURL}/${id}`);
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  postProduto(produto: Produto) {
    return this.http.post<ApiResponse<Produto>>(this.baseURL, produto);
  }

  putProduto(produto: Produto) {
    return this.http.put<ApiResponse<Produto>>(`${this.baseURL}/${produto.id}`, produto);
  }

  deleteProduto(id: number) {
    return this.http.delete<ApiResponse>(`${this.baseURL}/${id}`);
  }

}
