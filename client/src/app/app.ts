import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Todo } from './features/todo/todo';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Todo],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('ng-temp');
}
