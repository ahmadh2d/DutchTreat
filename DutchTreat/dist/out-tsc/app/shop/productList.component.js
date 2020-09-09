import { __decorate } from "tslib";
import { Component } from '@angular/core';
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.products = [];
    }
    ngOnInit() {
        this.data.loadProduct()
            .subscribe(success => {
            if (success)
                this.products = this.data.products;
        });
    }
};
ProductList = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html"
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map