import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ClientError } from '../ClientError';
import { Router } from '@angular/router';
import { Hero } from '../add-hero/HeroModel';
import { PageResult } from './PageResult';
@Injectable()
export class HeroService
{
  private baseUrl: string;
  public hero: Hero[];
  public pageNumber: number = 1;
  public Count: number;
  
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  GetHero()
  {
    this.http.get<PageResult<Hero>>('api/hero/get-hero').subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
}