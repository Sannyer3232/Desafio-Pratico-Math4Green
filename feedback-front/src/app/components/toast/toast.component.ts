import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService, ToastMessage } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div *ngIf="message" class="toast" [ngClass]="message.type">
      <div class="icon">
        <span *ngIf="message.type === 'success'">✅</span>
        <span *ngIf="message.type === 'error'">⚠️</span>
        <span *ngIf="message.type === 'info'">ℹ️</span>
      </div>
      <div class="content">{{ message.text }}</div>
      <button class="close-btn" (click)="close()">×</button>
    </div>
  `,
  styleUrls: ['./toast.component.css']
})
export class ToastComponent {
  message: ToastMessage | null = null;

  constructor(private toastService: ToastService) {

    this.toastService.toast$.subscribe(msg => this.message = msg);
  }

  close() {
    this.toastService.clear();
  }
}