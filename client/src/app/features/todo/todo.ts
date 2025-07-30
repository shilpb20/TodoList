import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-todo',
  templateUrl: './todo.html',
  styleUrls: ['./todo.css'],
  imports: [FormsModule],
})

export class Todo {
  title: string = '';
  description: string = '';

  onAdd() {
    console.log('Add clicked:', this.title, this.description);
    // You can later add code here to submit to backend

    // Clear inputs after submit
    this.title = '';
    this.description = '';
  }
}
