import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // Import CommonModule
import { CartService } from '../../services/cart.service';
import { Cart, CartItem } from '../../models/cart.model';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-cart',
  standalone: true, // Mark the component as standalone
  imports: [CommonModule], // Add CommonModule to imports
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartId = 1; // Example cart ID
  cart: Cart | null = null;
  totalPrice: number = 0;

  constructor(private cartService: CartService,private productService: ProductService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getCart(this.cartId).subscribe(
      (data) => {
        this.cart = data;
        console.log('Cart loaded:', this.cart);
        this.calculateTotalPrice();
      },
      (error) => console.error('Error loading cart', error)
    );
  }

  deleteItem(productId: number): void {
    if (this.cart) {
      this.cartService.removeProductFromCart(this.cartId, productId).subscribe(
        () => this.loadCart(),
        (error) => console.error('Error deleting item', error)
      );
    }
  }

  decreaseQuantity(item: CartItem): void {
    if (item.quantity > 1) {
      item.quantity--;
      this.updateItemQuantity(item.cartItemId, item.quantity);
    }
  }

  increaseQuantity(item: CartItem): void {
    item.quantity++;
    this.updateItemQuantity(item.cartItemId, item.quantity);
  }

  updateItemQuantity(cartItemId: number, quantity: number): void {
    this.cartService.updateCartItemQuantity(this.cartId,cartItemId, quantity).subscribe(
      () => this.calculateTotalPrice(),
      (error) => console.error('Error updating item quantity', error)
    );
  }

  calculateTotalPrice(): void {
    if (this.cart && this.cart.cartItems) {
      this.totalPrice = this.cart.cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);
    }
  }

  purchaseCart(): void {
    if (this.cart && this.cart.cartItems) {
      this.cartService.clearCart(this.cartId).subscribe(
        () => {
          this.loadCart();
          alert('Purchase successful!');
        },
        (error) => console.error('Error clearing cart', error)
      );
    }
  }
}
