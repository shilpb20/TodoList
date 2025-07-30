import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TodoService, TodoItem } from '../../core/services/todo.service';

@Component({
  standalone: true,
  selector: 'app-todo',
  templateUrl: './todo.html',
  styleUrls: ['./todo.css'],
imports: [FormsModule, CommonModule],
})

export class Todo implements OnInit {
  title = '';
  description = '';

  tasks: (TodoItem & { showDescription: boolean })[] = [];

    constructor(private todoService: TodoService) {
        console.log('Todo component constructed');
    } 

  ngOnInit(): void {
    this.todoService.getAll().subscribe({
      next: (todos) => {
        this.tasks = todos.map((todo) => ({ ...todo, showDescription: false }));
      },
      error: (err) => {
        console.error('Error fetching todos:', err);
      },
    });
  }

  toggleDesc(index: number): void {
    this.tasks[index].showDescription = !this.tasks[index].showDescription;
  }

  onAdd(): void {
    console.log('Submitted:', this.title, this.description);
    this.title = '';
    this.description = '';
  }

  deleteTask(index: number): void {
    this.tasks.splice(index, 1);
  }
}
