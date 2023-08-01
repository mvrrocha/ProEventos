import { PaginatedResult, Pagination } from './../../../models/Pagination';
import { environment } from './../../../../environments/environment.prod';
import { createContentChild } from '@angular/compiler/src/core';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';
import { Subject } from 'rxjs';
import { debounce, debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;
  message?: string;
  public eventoId: number = 0;
  public eventos: Evento[] = [];
  public pagination = {} as Pagination;
  larguraImagem: number = 100;
  margemImagem: number = 2;
  exibirImagem: boolean = true;

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarEventos(evt: any): void {
    if (this.termoBuscaChanged.observers.length == 0) {
      this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
        filtrarPor => {
          this.spinner.show();
          this.eventoService.getEventos(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filtrarPor
          ).subscribe(
            (paginatedResult: PaginatedResult<Evento[]>) => {
              this.eventos = paginatedResult.result;
              this.pagination = paginatedResult.pagination;
            },
            (error: any) => {
              this.spinner.hide();
              this.toastr.error('Erro ao carregar os Eventos', 'Erro!');
            }
          ).add(() => this.spinner.hide());
        }
      )
    }
    this.termoBuscaChanged.next(evt.value);
  }

  constructor(private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  public ngOnInit(): void {
    this.pagination = { currentPage: 1, itemsPerPage: 3, totalItems: 1 } as Pagination;
    this.CarregarEventos();
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }

  public mostraImagem(imagemURL: string): string {
    return (imagemURL != '') ? `${environment.apiURL}resources/images/${imagemURL}` : 'assets/img/semImagem.png';
  }

  public CarregarEventos(): void {
    this.spinner.show();
    this.eventoService.getEventos(this.pagination.currentPage,
      this.pagination.itemsPerPage).subscribe((paginatedResult: PaginatedResult<Evento[]>) => {
        this.eventos = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar os eventos!', 'Erro')
        }
      ).add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number) {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.CarregarEventos();
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (result: any) => {
        if (result.message == "Deletado") {
          this.toastr.success(`Evento de código ${this.eventoId} excluído com sucesso!`, 'Confirmação');
          this.CarregarEventos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o evento de código ${this.eventoId}`, 'Erro');
      }
    ).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
}
