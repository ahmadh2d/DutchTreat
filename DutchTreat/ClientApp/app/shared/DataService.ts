import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { Order, OrderItem } from './Order';
import { Product } from './Product';

@Injectable()
export class DataService {
	constructor(private http: HttpClient) {
	}

	public products: Product[] = [];
	public order: Order = new Order();

	loadProduct(): Observable<boolean> {
		return this.http.get('/api/product')
			.pipe(
				map((data: any[]) => {
					this.products = data;
					return true;
				})
			);
	}

	addToOrder(newProduct: Product) {
		var orderItem: OrderItem = this.order.items.find(i => i.productId == newProduct.id);

		if (orderItem) {
			orderItem.quantity += 1;
		}
		else {
			orderItem = new OrderItem();

			orderItem.productId = newProduct.id;
			orderItem.productArtId = newProduct.artId;
			orderItem.productArtist = newProduct.artist;
			orderItem.productCategory = newProduct.category;
			orderItem.productSize = newProduct.size;
			orderItem.productTitle = newProduct.title;
			orderItem.unitPrice = newProduct.price;
			orderItem.quantity = 1;

			this.order.items.push(orderItem);
		}

	}
}