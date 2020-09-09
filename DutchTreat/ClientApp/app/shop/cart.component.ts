﻿import { Component } from "@angular/core";
import { DataService } from '../shared/DataService';

@Component({
	selector: "the-cart",
	templateUrl: "cart.component.html",
	styleUrls: ["cart.component.css"]
})
export class Cart {
	constructor(public data: DataService) {
	}
}