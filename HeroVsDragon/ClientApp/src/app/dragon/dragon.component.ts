import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { error } from 'protractor';
import { HeroService } from '../add-hero/HeroService';
import { ClientError } from '../ClientError';
import { PageResult } from '../hero-main/PageResult';
import { Dragon } from './DragonModel';

@Component({
  selector: 'app-dragon',
  templateUrl: './dragon.component.html',
  styleUrls: ['./dragon.component.css']
})
export class DragonComponent implements OnInit {
  defaultSelect = '1';
  params: QuantityDragon = new QuantityDragon();
  hitDragon: HitDragon = new HitDragon();
  public dragon: Dragon[];
  public pageNumber: number = 1;
  public Count: number;
  private IdUser: string;
  constructor(private heroService: HeroService, private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
      this.IdUser = sessionStorage.getItem('IDHero');
  }

  ngOnInit() {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
  }
  createDragon(form: NgForm)
  {
    this.params.quantityDragon = form.value.quantity
    this.http.post('api/dragon/create', this.params).subscribe(
      result => {
      }
    );
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
  }
  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon?page=' + pageNumber).subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }
  searchIdDragon(form:NgForm): void
  { 
    let params = new HttpParams();
    params = params.append('idDragon',form.value.id);
    this.http.get<PageResult<Dragon>>('api/dragon/get-search-id', {params: params}).subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  Discharge()
  {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
  }
  sortDesc()
  {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon-sort-desc').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  sortAsc()
  {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon-sort-asc').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  searchName(form:NgForm): void
  {
    let params = new HttpParams();
    params = params.append('textSearch',form.value.textSearch);
    params = params.append('paramsFilter',form.value.lengthName);
    this.http.get<PageResult<Dragon>>('api/dragon/get-search-name', {params: params}).subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  searchHP(form:NgForm)
  {
    let params = new HttpParams();
    params = params.append('hpSearch',form.value.textSearch);
    params = params.append('paramsFilter',form.value.lengthHP);
    this.http.get<PageResult<Dragon>>('api/dragon/get-search-hp', {params: params}).subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }
  searchRemnant(form:NgForm)
  {
    let params = new HttpParams();
    params = params.append('textSearch',form.value.textSearch);
    params = params.append('paramsFilter',form.value.lengthRemnant);
    this.http.get<PageResult<Dragon>>('api/dragon/get-search-remnant', {params: params}).subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
      }, 
      error => console.error(error));
  }

  LoadDragon()
  {
    this.http.get<PageResult<Dragon>>('api/dragon/get-dragon').subscribe(result => {
      this.dragon = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    },
      error => console.error(error));
  }

  HitDragon(id: string)
  {
    this.hitDragon.idDragon = id.toString();
    this.hitDragon.idHero = this.IdUser.toString();
    this.http.post('api/hit/hit-dragon', this.hitDragon).subscribe(
      result => 
      {
      const message: ClientError = <ClientError>result;
      alert(message.message);
      }
    );
    this.LoadDragon()
  }
}

export class QuantityDragon
{
  quantityDragon: string;
}
export class HitDragon
{
  idHero: string;
  idDragon: string;
}
