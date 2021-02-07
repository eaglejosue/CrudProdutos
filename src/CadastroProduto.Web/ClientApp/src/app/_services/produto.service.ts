import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
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
    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    const req = new HttpRequest('POST', `${this.baseURL}/upload`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(req);
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
