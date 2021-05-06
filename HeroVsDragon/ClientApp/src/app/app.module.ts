import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginService } from './home/LoginService';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddHeroComponent } from './add-hero/add-hero.component';
import { HeroService } from './add-hero/HeroService';
import { ReactiveFormsModule }   from '@angular/forms';
import { HeroMainComponent } from './hero-main/hero-main.component';
import {NgxPaginationModule} from 'ngx-pagination';
import {MatSortModule} from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { DragonComponent } from './dragon/dragon.component' 
import { DragonService } from './dragon/DragonService';
import { HeroVsdragonComponent } from './hero-vsdragon/hero-vsdragon.component';
import { JwtInterceptor } from './jwt-interceptor';
import { NavMenuMainComponent } from './nav-menu-main/nav-menu-main.component';
const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'add-hero', component: AddHeroComponent },
  { path: 'hero', component: HeroMainComponent },
  { path: 'dragon', component: DragonComponent },
  { path: 'hero-vs-dragon', component: HeroVsdragonComponent}

 
]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AddHeroComponent,
    HeroMainComponent,
    DragonComponent,
    HeroVsdragonComponent,
    NavMenuMainComponent

    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    NgxPaginationModule,
    MatSortModule,
    MatTableModule
  ],
  providers: [LoginService,HeroService, DragonService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
