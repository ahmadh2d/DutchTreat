import * as _ from "lodash"

export class Order {
    orderId: number;
    orderNumber: string;
    orderDate: Date = new Date();
    items: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {
        return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
	}
}

export class OrderItem {
    id: number;
    unitPrice: number;
    quantity: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtId: string;
    productArtist: string;
}
