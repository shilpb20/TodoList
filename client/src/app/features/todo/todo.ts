import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-todo',
  templateUrl: './todo.html',
  styleUrls: ['./todo.css'],
  imports: [FormsModule, CommonModule],
})
export class Todo {
  title = '';
  description = '';

  tasks = [
    {
      id: 1,
      title: 'Buy groceries',
      description: 'Milk, Bread, Eggs',
      showDescription: false,
    },
    {
      id: 2,
      title: 'Call plumber',
      description: '',
      showDescription: false,
    },
  ];

  toggleDesc(index: number): void {
    this.tasks[index].showDescription = !this.tasks[index].showDescription;
  }

  onAdd(): void {
    // For now, just log input
    console.log('Submitted:', this.title, this.description);
    this.title = '';
    this.description = '';
  }

  deleteTask(index: number): void {
    this.tasks.splice(index, 1);
  }
}
