﻿using Application.Core;
using Application.UserReports.DTOs;
using MediatR;

namespace Application.UserReports.GetAllUserReports;

public record GetAllUserReportsQuery : IRequest<Result<List<UserReportDto>>>;
