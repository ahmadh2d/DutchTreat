import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/DataService';

@Component({
	selector: "app-login",
	templateUrl: "login.component.html",
	styleUrls: []
})
export class Login {
	constructor(private data: DataService, private router: Router) {
	}

	public errorMessage: string = "";

	public creds = {
		username: "",
		password: ""
	}

	public onLogin(): void {
		this.data.login(this.creds)
			.subscribe(success => {
				if (success) {
					if (this.data.order.items.length == 0) {
						this.router.navigate([""]);
					}
					else {
						this.router.navigate(["checkout"]);
					}
				} else {

				}
			}, err => this.errorMessage = "Failed to login");
	}
}