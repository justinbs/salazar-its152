import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, forkJoin, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Post } from '../model/post.type';

@Injectable({ providedIn: 'root' })
export class PostsService {
  private base = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getPost(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.base}/Post/${id}`);
  }

  getPosts(): Observable<Post[]> {
    const ids = environment.seedPostIds;
    const calls = ids.map(id =>
      this.getPost(id).pipe(catchError(() => of(null as unknown as Post)))
    );
    return forkJoin(calls).pipe(map(list => list.filter(p => p)));
  }
}
