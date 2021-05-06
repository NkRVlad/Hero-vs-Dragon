import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HeroService } from './HeroService';
import { FormGroup, FormControl , Validators} from '@angular/forms';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent implements OnInit {
  
  nickname: string;
  invalidHero: boolean;
  invalidHeroLength: boolean;
  myForm : FormGroup;
  constructor(private heroService: HeroService) { 
    
    this.myForm = new FormGroup({
    "userName": new FormControl("", [
         Validators.pattern('[a-zA-Z ]*')
    ] )
  });}

  ngOnInit() {
  }

  create(form: NgForm): void {
   
    if(form.value.userName.length != 0)
    {
      if(form.value.userName.length >=4 && form.value.userName.length <= 20)
      {
        this.heroService.createHero(form);
        this.invalidHero = false;
        this.invalidHeroLength = false;
      }
      else
      {
        this.invalidHeroLength = true;
      }
    }
    else 
    {
      this.invalidHero = true;
    }
    
  }
  
}
