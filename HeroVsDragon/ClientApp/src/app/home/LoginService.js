"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginService = void 0;
var LoginService = /** @class */ (function () {
    function LoginService(http) {
        this.http = http;
        this.rootUrl = "/";
    }
    LoginService.prototype.LoginValid = function (login) {
        return this.http.post(this.rootUrl, login);
    };
    return LoginService;
}());
exports.LoginService = LoginService;
//# sourceMappingURL=LoginService.js.map
