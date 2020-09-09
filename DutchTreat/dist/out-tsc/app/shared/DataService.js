import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.products = [];
    }
    loadProduct() {
        return this.http.get('/api/product')
            .pipe(map((data) => {
            this.products = data;
            return true;
        }));
    }
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=DataService.js.map