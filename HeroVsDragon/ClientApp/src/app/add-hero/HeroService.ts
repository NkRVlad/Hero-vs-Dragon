import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Hero } from './HeroModel';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ClientError } from '../ClientError';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
@Injectable()
export class HeroService
{
  private baseUrl: string;
  heroTempInfo: InfoHero = new InfoHero();
  private hero: Hero = new Hero();
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    
   

  }

  createHero(form: NgForm): void
  {
    this.hero.nickname = form.value.userName
    
    this.http.post<InfoHero>('api/hero/create', this.hero).subscribe(
      result => {
        this.heroTempInfo = result;
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

export class InfoHero
{
  public idUser: string;
}