import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

interface Task {
  title: string;
  description?: string;
  showDescription: boolean;
}

@Component({
  standalone: true,
  selector: 'app-todo',
  templateUrl: './todo.html',
  styleUrls: ['./todo.css'],
  imports: [FormsModule],
})
export class Todo {
  title = '';
  description = '';

  tasks: Task[] = [
    {
      title: 'Example Task',
      description: 'Add your notes here...',
      showDescription: false,
    },
  ];

  toggleDesc(index: number) {
    this.tasks[index].showDescription = !this.tasks[index].showDescription;
  }

  deleteTask(index: number) {
    this.tasks.splice(index, 1);
  }

  onAdd() {
    if (!this.title.trim()) return;

    this.tasks.push({
      title: this.title.trim(),
      description: this.description.trim(),
      showDescription: false,
    });

    this.title = '';
    this.description = '';
  }
}
