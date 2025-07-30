export interface TodoItem {
  id: string;
  title: string;
  description?: string;
}

export interface CreateTodoItem {
  title: string;
  description?: string;
}