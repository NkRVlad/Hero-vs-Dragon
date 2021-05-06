import { Component } from '@angular/core';
import { Login } from './Login';
import { LoginService } from './LoginService';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  nickname: string;
  invalidLogin: boolean;
  constructor(private loginService: LoginService) { }

  login(form: NgForm): void {
   
    if(form.value.userName.length != 0)
    {
      this.loginService.LoginValid(form);
      this.invalidLogin = false;
    }
    else 
    {
      this.invalidLogin = true;
    }
    
  }
}
