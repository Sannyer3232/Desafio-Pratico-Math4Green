import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FeedbackService } from '../../services/feedback.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './board.component.html',
  styleUrl: './board.component.css'
})
export class BoardComponent implements OnInit {
  suggestions: any[] = [];
  isModalOpen = false;
  

  newSuggestion = {
    title: '',
    description: '',
    userId: 0 
  };

  
  currentUserId: number = 0;

  constructor(
    private feedbackService: FeedbackService,
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService
  ) {}

  ngOnInit() {
    this.loadSuggestions();
    this.decodeUser();
  }

 
  decodeUser() {
    const token = this.authService.getToken();
    if (token) {
      try {
       
        const payload = JSON.parse(atob(token.split('.')[1]));
        this.currentUserId = Number(payload.nameid); 
      } catch (e) {
        console.error('Erro ao ler token', e);
      }
    }
  }

  loadSuggestions() {
    this.feedbackService.getSuggestions().subscribe({
      next: (data) => this.suggestions = data,
      error: (err) => console.error('Erro ao carregar', err)
    });
  }

  vote(suggestionId: number) {
    const voteDto = {
      suggestionId: suggestionId,
      userId: this.currentUserId
    };

    this.feedbackService.vote(voteDto).subscribe({
      next: () => {
 
        this.loadSuggestions();

        this.toastService.show('Voto computado!', 'success');
      },
      error: (err) => {
  
        const msg = err.error.message || 'Erro ao votar';
        this.toastService.show(msg, 'error');
      }
    });
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.newSuggestion = { title: '', description: '', userId: 0 };
  }

  submitSuggestion() {

    const dto = {
      ...this.newSuggestion,
      userId: this.currentUserId
    };

    this.feedbackService.createSuggestion(dto).subscribe({
      next: () => {
        this.closeModal();
        this.loadSuggestions(); 
        this.toastService.show('Sugestão criada com sucesso!', 'success');
      },
      error: (err) => {
        this.toastService.show('Erro ao criar sugestão.', 'error');
      }
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}