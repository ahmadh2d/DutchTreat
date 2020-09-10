import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { DataService } from '../shared/DataService';

@Component({
	selector: "the-cart",
	templateUrl: "cart.component.html",
	styleUrls: ["cart.component.css"]
})
export class Cart {
	constructor(public data: DataService, private router: Router) {
	}

	onCheckout() {
		if (this.data.loginRequired) {
			this.router.navigate(["login"]);
		}
		else {
			this.router.navigate(["checkout"]);
		}
	}
}