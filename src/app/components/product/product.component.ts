import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  searchTerm: string = "";  // Stores the current search input
  cartId = 1;  // Example cart ID

  constructor(private productService: ProductService, private cartService: CartService) {}

  ngOnInit() {
    this.fetchProducts();
  }

  fetchProducts() {
    console.log('Fetching products...');  // Log that products are being fetched
    console.log('search term:', this.searchTerm);  // Log the search term

    this.loading = true;
    this.productService.getProducts(this.searchTerm).subscribe(products => {
      console.log('Fetched products:', products);  // Log the fetched products
      this.products = products;
      this.loading = false;
    }, error => {
      console.error('Error fetching products:', error);  // Log any errors
      this.loading = false;
    });
  }

  // Triggers search when the button is clicked
  onSearchClick() {
    this.fetchProducts();
  }

  // Adds a product to the cart
  addToCart(ProductId: number) {
    console.log(`Adding product ${ProductId} to cart ${this.cartId}`);
    const product = this.products.find(p => p.productId === ProductId);
    if (product) {
      this.cartService.addProductToCart(this.cartId, ProductId, 1, product.price, product.name, product.imageURL).subscribe(
        () => {
          console.log(`Product ${ProductId} added to cart ${this.cartId}`);
          alert('Product added to cart');
        },
        (error) => console.error('Error adding product to cart:', error)
      );
    } else {
      console.error('Product not found');
    }
  }
}