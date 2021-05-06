import { Time } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { HeroService } from '../add-hero/HeroService';
import { Hero } from './HeroModel';
import { PageResult } from './PageResult';

@Component({
  selector: 'app-hero-main',
  templateUrl: './hero-main.component.html',
  styleUrls: ['./hero-main.component.css']
})
export class HeroMainComponent implements OnInit {

  private baseUrl: string;
  public hero: Hero[];
  public pageNumber: number = 1;
  public Count: number;
  defaultTime = '12:00';
  private IdUser: string;
  constructor(private heroService: HeroService, private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
    this.http.get<PageResult<Hero>>('api/hero/get-hero').subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
      this.IdUser = sessionStorage.getItem('IDHero');
  }

  ngOnInit() {
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Hero>>('api/hero/get-hero?page=' + pageNumber).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  
  sortAsc()
  {
    this.http.get<PageResult<Hero>>('api/hero/get-hero-sort-asc').subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  sortDesc()
  {
    this.http.get<PageResult<Hero>>('api/hero/get-hero-sort-desc').subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  searchNickname(form: NgForm): void 
  {
    let params = new HttpParams();
    params = params.append('textSearch',form.value.textSearch);
    params = params.append('paramsFilter',form.value.lengthNickname);
    this.http.get<PageResult<Hero>>('api/hero/get-search-nickname', {params: params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  searchTime(form: NgForm): void
  {
    let params = new HttpParams();
    params = params.append('timeSearch',form.value.time);
    params = params.append('paramsFilter',form.value.timeFilter);
    this.http.get<PageResult<Hero>>('api/hero/get-search-time', {params: params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  Discharge()
  {
    this.http.get<PageResult<Hero>>('api/hero/get-hero').subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
}
