export interface Paciente {
    id: string,
    pacienteId: number,
    apellidos: string,
    nombres: string,
    direccion: string,
    telefono: string,
    genero: string,
    estado: number,
    observaciones: string[]
}