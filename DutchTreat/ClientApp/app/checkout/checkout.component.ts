import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { DataService } from '../shared/DataService';
import { Order } from '../shared/Order';

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {

	constructor(public data: DataService, private router: Router) {
	}

	public errorMessage: string = "";

	onCheckout() {
        this.data.checkout()
			.subscribe(success => {
				if (success) {
					this.router.navigate([""]);
				}
			}, err => this.errorMessage = "Order Failed")
  }
}