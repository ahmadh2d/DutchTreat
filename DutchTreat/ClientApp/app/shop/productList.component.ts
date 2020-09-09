import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/DataService';
import { Product } from '../shared/Product';

@Component({
	selector: "product-list",
	templateUrl: "productList.component.html",
	styleUrls: ["productList.component.css"]
})

export class ProductList implements OnInit {
	constructor(private data: DataService) {
	}

	public products: Product[] = [];

	ngOnInit(): void {
		this.data.loadProduct()
			.subscribe(success => {
				if (success)
					this.products = this.data.products
			});
	}

	addProduct(product: Product) {
		this.data.addToOrder(product);
	}
}