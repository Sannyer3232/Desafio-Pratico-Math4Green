import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private apiUrl = 'http://localhost:5262/api/Suggestion';

  constructor(private http: HttpClient) { }

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`
      })
    };
  }

  getSuggestions(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createSuggestion(suggestion: any): Observable<any> {

    return this.http.post(this.apiUrl, suggestion, this.getAuthHeaders());
  }

  vote(voteDto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/vote`, voteDto, this.getAuthHeaders());
  }
}
