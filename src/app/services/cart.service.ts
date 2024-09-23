import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddItemToCartRequest, Cart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5291/api/cart';  // Replace with your API URL

  constructor(private http: HttpClient) { }

  getCart(cartId: number): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/${cartId}`);
  }



  addProductToCart(cartId: number, productId: number, quantity: number, price: number, name: string, imageURL: string): Observable<void> {
    const requestBody: AddItemToCartRequest = {
      quantity,
      price,
      name,
      imageURL
    };
    return this.http.post<void>(`${this.apiUrl}/${cartId}/add/${productId}`, requestBody);
  }

  removeProductFromCart(cartId: number, productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${cartId}/remove/${productId}`);
  }
  
  updateCartItemQuantity(cartId: number, itemId: number, quantity: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${cartId}/update/${itemId}`, quantity);
  }

  clearCart(cartId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${cartId}/clear`);
  }
}
