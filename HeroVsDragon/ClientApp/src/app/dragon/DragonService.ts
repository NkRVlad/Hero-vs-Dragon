import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ClientError } from '../ClientError';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HitDragon } from './dragon.component';
@Injectable()
export class DragonService
{
  private baseUrl: string;

  constructor(private router: Router,private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    
  }

  public HitDragon(hit: HitDragon)
  {
    return this.http.post('api/hit/test-hit-dragon', hit ).subscribe();
  }
}