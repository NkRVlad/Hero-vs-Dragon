import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Login } from './Login';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ClientError } from '../ClientError';
import { Router } from '@angular/router';
import { HeroService, InfoHero } from '../add-hero/HeroService';

export class LoginService
{
  private baseUrl: string;
  private idUser: Object;
  private login: Login = new Login();
  _heroService: HeroService;
  heroTempInfo: InfoHero = new InfoHero();
  constructor(private heroService: HeroService, private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this._heroService = heroService;
  }

  LoginValid(form: NgForm): void {
    
    this.login.nickname = form.value.userName
    
    this.http.post<InfoHero>('api/login', this.login).subscribe(
      result => {
      this.heroTempInfo =  result;
      sessionStorage.setItem('IDHero', this.heroTempInfo.idUser);
      const token = (<any>result).token;
      localStorage.setItem('jwt', token);
      this.router.navigate(['/hero']);
      },
      (error:HttpErrorResponse) => { 
        if(error.status===400){            
           const errors: ClientError = error.error;
              alert(errors.message);
        }
      } 
    );
  }
}
