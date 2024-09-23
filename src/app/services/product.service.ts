import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5291/api/product';  // Replace with your API URL

  constructor(private http: HttpClient) { }

  getProducts(searchTerm: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}?searchTerm=${encodeURIComponent(searchTerm)}`);
  }
  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${product.productId}`, product);
  }

  updateProductQuantity(productId: number, quantity: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${productId}/update-quantity`, { quantity });
  }
  
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}