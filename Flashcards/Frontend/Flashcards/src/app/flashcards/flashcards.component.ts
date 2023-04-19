import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-flashcards',
  templateUrl: './flashcards.component.html',
  styleUrls: ['./flashcards.component.css']
})
export class FlashcardsComponent implements OnInit {
  cards : Card[] = [
  { question : "What is my name?",
    answer : "Sonya",
    viewAnswer: false  
  },
  { question : "What is my favorite color?",
    answer : "Red",
    viewAnswer: false  
  },
  { question : "What is my favorite cuisine?",
    answer : "Japanese",
    viewAnswer: true  
  }
  ];
  toggleCards : boolean = true; // if true show card view
  
  ngOnInit(): void {
    // get all flashcards
  }

  toggleView() {
    this.toggleCards = !this.toggleCards;
  }

  toggleCardAnswer(fc : Card) {
    console.log(fc.question)
    fc.viewAnswer = !fc.viewAnswer;
  }

}

export class Card{
  question : string = "";
  answer : string = "";
  viewAnswer : boolean = false;
}