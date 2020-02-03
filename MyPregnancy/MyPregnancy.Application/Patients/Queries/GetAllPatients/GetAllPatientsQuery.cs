namespace MyPregnancy.Application.Patients.Queries.GetAllPatients
{
    using MediatR;
    using System.Collections.Generic;
    using Common.Dtos;

    public class GetAllPatientsQuery : QueryableArgsBase, IRequest<IEnumerable<PatientDto>>
    {
    }
}
