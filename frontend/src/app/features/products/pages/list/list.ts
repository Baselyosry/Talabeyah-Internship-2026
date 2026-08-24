import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { ProductCard } from '../../components';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, ProductCard],
  templateUrl: './list.html',
  styleUrls: ['./list.scss']
})
export class ProductList implements OnInit {

  private productService = inject(ProductService);
  products = signal<Product[]>([]);
  loading = signal(false);
  errorMessage = signal('');

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.loading.set(false);
    this.errorMessage.set('');

    this.productService.getAll().subscribe({
      next: (data) => {
        this.loading.set(false);
        this.products.set(data);
      },
      error: () => {
        this.loading.set(false);
        this.errorMessage.set('Something went wrong while loading products.');
      }
    });
  }
}

