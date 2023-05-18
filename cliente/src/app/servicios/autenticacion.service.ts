import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { IAutenticacion } from '../models/autenticacionToken/IAutenticacion'
import { BehaviorSubject, map } from 'rxjs'
import { IUsuario } from '../models/autenticacionToken/IUsuario'

@Injectable({
  providedIn: 'root',
})
export class AutenticacionService {
  private usuarioSubject: BehaviorSubject<IUsuario>

  public get usuario(): IUsuario {ssxxsxxd 
    return this.usuarioSubject.value
  }

  constructor(private http: HttpClient) {
    this.usuarioSubject = new BehaviorSubject<IUsuario>(
      JSON.parse(localStorage.getItem('usuario')!)
    )
  }

  login(correo: string, password: string) {
    return this.http
      .post<IAutenticacion>('https://localhost:7260/api/login', {
        correo,
        password,
      })
      .pipe(
        map((resp) => {
          if (resp.estado === 'EXITO') {
            localStorage.setItem('usuario', JSON.stringify(resp.datos))
            this.usuarioSubject?.next(resp.datos!)
          }
          return resp
        })
      )
  }

  // logiado() {
  //   this.login().subscribe((respuesta) => {
  //     this.token = respuesta.datos?.token
  //   })

  //   return this.token
  // }
}
