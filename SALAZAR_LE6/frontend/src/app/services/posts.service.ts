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

  private normalize(obj: any): Post {
  // Ensure fields are always defined
  const idRaw = obj?.id ?? obj?.Id ?? 0;
  return {
    id: typeof idRaw === 'string' ? parseInt(idRaw, 10) : Number(idRaw),
    title: obj?.title ?? obj?.Title ?? '',
    body: obj?.body ?? obj?.Body ?? ''
  } as Post;
}


  getPost(id: number): Observable<Post> {
    return this.http.get<any>(`${this.base}/Post/${id}`).pipe(
      map(data => this.normalize(data))
    );
  }

  getPosts(): Observable<Post[]> {
    const calls = environment.seedPostIds.map(id =>
      this.getPost(id).pipe(
        catchError(() => of(null as unknown as Post)) // tolerate missing/401 ids
      )
    );
    return forkJoin(calls).pipe(
      map(list => list.filter(p => p)) // drop nulls
    );
  }
}
