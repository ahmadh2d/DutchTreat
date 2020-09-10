import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { Order, OrderItem } from './Order';
import { Product } from './Product';

@Injectable()
export class DataService {
	constructor(private http: HttpClient) {
	}

	private token: string = "";
	private tokenExpiration: Date;

	public products: Product[] = [];
	public order: Order = new Order();

	public get loginRequired(): boolean {
		return (this.token.length == 0 || this.tokenExpiration > new Date());
	}

	loadProduct(): Observable<boolean> {
		return this.http.get('/api/product')
			.pipe(
				map((data: any[]) => {
					this.products = data;
					return true;
				})
			);
	}

	public login(creds): Observable<boolean> {
		return this.http.post("/Account/CreateToken", creds)
			.pipe(map((data: any) => {
				this.token = data.token;
				this.tokenExpiration = data.expiration;
				return true;
			}));
	}

	public checkout(): Observable<boolean> {
		if (!this.order.orderNumber) {
			this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString(); 
		}

		return this.http.post("/api/orders", this.order, {
			headers: new HttpHeaders({ "Authorization": "Bearer " + this.token })
		})
			.pipe(map(response => {
				this.order = new Order();
				return true;
			}));
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