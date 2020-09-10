import { __decorate } from "tslib";
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Order, OrderItem } from './Order';
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.token = "";
        this.products = [];
        this.order = new Order();
    }
    get loginRequired() {
        return (this.token.length == 0 || this.tokenExpiration > new Date());
    }
    loadProduct() {
        return this.http.get('/api/product')
            .pipe(map((data) => {
            this.products = data;
            return true;
        }));
    }
    login(creds) {
        return this.http.post("/Account/CreateToken", creds)
            .pipe(map((data) => {
            this.token = data.token;
            this.tokenExpiration = data.expiration;
            return true;
        }));
    }
    checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear.toString() + this.order.orderDate.getTime().toString();
        }
        return this.http.post("/api/orders", this.order, {
            headers: new HttpHeaders({ "Authorization": "Bearer " + this.token })
        })
            .pipe(map(response => {
            this.order = new Order();
            return true;
        }));
    }
    addToOrder(newProduct) {
        var orderItem = this.order.items.find(i => i.productId == newProduct.id);
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
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=DataService.js.map