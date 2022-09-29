import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(public fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmeSenha')
    };

    this.form = this.fb.group({
      titulo: ['', Validators.required],
      primeiroNome: ['', [Validators.required, Validators.minLength(3)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required],
      funcao: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(20)]],
      senha: ['', Validators.minLength(6)],
      confirmeSenha: ['', null]
    }, formOptions);
  }

  onSubmit(): void{
    if(this.form.invalid) {
      return;
    }
  }

  public resetForm(event: any): void {
    event?.preventDefault();
    this.form.reset();
  }
}
