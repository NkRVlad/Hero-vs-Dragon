import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PageResult } from '../hero-main/PageResult';
import { HeroVsdragon } from './HeroVsdragonModel';

@Component({
  selector: 'app-hero-vsdragon',
  templateUrl: './hero-vsdragon.component.html',
  styleUrls: ['./hero-vsdragon.component.css']
})
export class HeroVsdragonComponent implements OnInit {
  
  private IdUser: string;
  public hero: HeroVsdragon[];
  public pageNumber: number = 1;
  public Count: number;
  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
    this.IdUser = sessionStorage.getItem('IDHero');

    let params = new HttpParams();
    params = params.append('IdHero', this.IdUser.toString());
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-hit-damage-dragon', {params: params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
  }
  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-hit-damage-dragon?page=' + pageNumber).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  ngOnInit() {
  }
  sortNameAsc()
  {
    let params = new HttpParams();
    params = params.append('IdHero', this.IdUser.toString());
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-name-dragon-sort-asc', {params:params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  sortNameDesc()
  {
    let params = new HttpParams();
    params = params.append('IdHero', this.IdUser.toString());
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-name-dragon-sort-desc', {params:params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  sortDamageAsc()
  {
    let params = new HttpParams();
    params = params.append('IdHero', this.IdUser.toString());
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-damage-dragon-sort-asc', {params:params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  sortDamageDesc()
  {
    let params = new HttpParams();
    params = params.append('IdHero', this.IdUser.toString());
    this.http.get<PageResult<HeroVsdragon>>('api/hero/get-damage-dragon-sort-desc', {params:params}).subscribe(result => {
      this.hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
}
