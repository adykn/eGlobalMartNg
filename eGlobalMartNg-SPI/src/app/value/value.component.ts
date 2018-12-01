import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
 values: any;
  constructor(private Http: HttpClient) { }

  ngOnInit() {
    this.getVales();
  }
 getVales(){
   this.Http.get('http://localhost:5000/api/Values').subscribe(
     resp => {this.values = resp; } ,
     error => { console.log(error); }
     );
 }
}
