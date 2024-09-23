import { Component, OnInit } from '@angular/core';
import {RouterLink, RouterModule } from '@angular/router';  // Import RouterModule
import { ProductComponent } from './components/product/product.component';
import { HttpClientModule } from '@angular/common/http';  
import { CartComponent } from './components/cart/cart.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [HttpClientModule,RouterModule, ProductComponent, RouterLink , CartComponent]  // Add RouterModule to imports
})
export class AppComponent implements OnInit {
  title = 'shopping-cart-app';

  ngOnInit() {
  }
}