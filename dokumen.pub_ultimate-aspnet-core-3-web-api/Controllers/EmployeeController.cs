using Microsoft.AspNetCore.Mvc;
using Contracts.Interface;
using Entity.Model;
using LoggerService;
using AutoMapper;
using System;
using System.Collections.Generic;
using Entity.DataTransferObject;
using Entity.Paging;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EmployeeController : ControllerBase
    {
         private readonly ILoggerManger _logger;
         private readonly IMapper _mapper;
         private readonly IMangeRepository _mangeRepository;
                        public EmployeeController( ILoggerManger loggerManger,IMangeRepository mangeRepository,IMapper mapper)
                        {
                            _logger = loggerManger;
                            _mangeRepository=mangeRepository;
                            _mapper=mapper;
                        }
                        [HttpGet("Company/{CompanyId}/Employees")]
                        public IActionResult GetEmployeesFroCompany(Guid CompanyId )
                        {
                             var Employees= _mangeRepository.employeeRepository.GetEmployeesByCompany(CompanyId,false);
                             var employeeDto=_mapper.Map<List< EmployeeDto>>(Employees);
                             return Ok( employeeDto);
                        }
                        [HttpGet("Company/{CompanyId}Employee{Id}")]
                        public IActionResult GetEmployeeFroCompany(Guid CompanyId,Guid Id )
                        {    var company=_mangeRepository.componyRepository.GetCompany(CompanyId,false);
                            if (company==null)
                             {
                                 _logger.LogInfo($"company  with id: {CompanyId} doesn't exist in the database.");
                                 return NotFound();
                             }
                             var Employee= _mangeRepository.employeeRepository.GetEmployeeByCompany(CompanyId,Id,false);
                             if (Employee==null)
                             {
                                 _logger.LogInfo($"Employe  with id: {Id} doesn't exist in the database. in company {company.Name}");
                                 return NotFound();
                             }
                             var employeeDto=_mapper.Map<EmployeeDto>(Employee);
                             return Ok( employeeDto);
                        }
                        [HttpPost("Company/{CompanyId}/CreateEmploy")]
                        public IActionResult CreateEmployee(Guid CompanyId,AddEmployeeDto AddEmployeeDto)
                        {
                            if(AddEmployeeDto == null)
                            {
                                _logger.LogError("thier is an error");
                                return NotFound();
                            }
                            var company=_mangeRepository.componyRepository.FindByCondation(del=>del.Id.Equals(CompanyId),false);
                            if(company == null)
                            {
                                _logger.LogError("thier is an error");
                                return NotFound();
                            }
                            var Employee=_mapper.Map<Employee>(AddEmployeeDto);
                            _mangeRepository.employeeRepository.CreateEmployee(CompanyId,Employee);
                            _mangeRepository.Save();
                            var EmployeeDto=_mapper.Map<EmployeeDto>(Employee);
                            return Ok(EmployeeDto);
                        }
                        [HttpPost("Company/{CompanyId}/CreateEmployies")]
                        public IActionResult CreateEmployies(Guid CompanyId,IEnumerable< AddEmployeeDto> AddEmployeeDto)
                        {
                            if(AddEmployeeDto == null)
                            {
                                _logger.LogError("thier is an error");
                                return NotFound();
                            }
                            var company=_mangeRepository.componyRepository.FindByCondation(del=>del.Id.Equals(CompanyId),false);
                            if(company == null)
                            {
                                _logger.LogError("thier is an error");
                                return NotFound();
                            }
                            var Employee=_mapper.Map<IEnumerable<Employee>>(AddEmployeeDto);
                            foreach (var item in Employee)
                            {
                            _mangeRepository.employeeRepository.CreateEmployee(CompanyId,item);
                            }
                            _mangeRepository.Save();
                            var EmployeeDto=_mapper.Map<IEnumerable<EmployeeDto>>(Employee);
                            return Ok(EmployeeDto);
                        }
                    [HttpDelete("Company/{CompanyId}/DeleteEmployeeFromCompany/{EmployeeId}")]
                    public IActionResult DeleteEmployeeFromCompany(Guid CompanyId,Guid EmployeeId)
                    {
                        var company=_mangeRepository.componyRepository.GetCompany(CompanyId,false);
                        if(company==null)
                        {
                            _logger.LogError("error");
                            return NotFound();
                        }
                        var employee=_mangeRepository.employeeRepository.GetEmployee(EmployeeId,false);
                         if(employee==null)
                        {
                            _logger.LogError("error");
                            return NotFound();
                        }
                        _mangeRepository.employeeRepository.Delete(employee);
                        _mangeRepository.Save();
                        return Ok(employee);
                    }
                [HttpPut("Company/{CompanyId}/UpdateEmployee/{EmployeeId}")]
                public IActionResult UpdateEmployee(Guid CompanyId,Guid EmployeeId,UpdateEmployeeDto updateEmployeeDto)
                {
                    var company=_mangeRepository.componyRepository.GetCompany(CompanyId,false);
                    if(company==null)
                    {
                        _logger.LogError("error");
                        return NotFound();
                    }
                    var employee=_mangeRepository.employeeRepository.GetEmployee(EmployeeId,false);
                    if(employee==null)
                    {
                        _logger.LogError($"error there is no employee with if{EmployeeId}");
                        return NotFound();
                    }
                    var employeeEntity=_mangeRepository.employeeRepository.GetEmployee(EmployeeId,true);
                    _mapper.Map(updateEmployeeDto,employeeEntity);
                    _mangeRepository.Save();
                    return Ok();
                }
                [HttpGet("Company/{CompanyId}/GetEmployeePaging")]
                public async Task< IActionResult> GetEmployeePaging(Guid CompanyId,[FromQuery] EmployeePrameter employeePrameter)
                {

                    var Employees=await _mangeRepository.employeeRepository.GetEmployeesByCompany(CompanyId,employeePrameter,false);
                             var employeeDto=_mapper.Map<IEnumerable< EmployeeDto>>(Employees);
                             return Ok( employeeDto);
                }
                  [HttpGet("Company/{CompanyId}/GetEmployeePaging2")]
                public async Task< IActionResult> GetEmployeePaging2(Guid CompanyId,[FromQuery] EmployeePrameter employeePrameter)
                {

                    var Employees=await _mangeRepository.employeeRepository.GetEmployeesByCompanyPaging(CompanyId,employeePrameter,false);
                    Response.Headers.Add("Ex-Pagnation",JsonConvert.SerializeObject(Employees.metaData));
                    Console.WriteLine(JsonConvert.SerializeObject(Employees.metaData));
                             var employeeDto=_mapper.Map<IEnumerable< EmployeeDto>>(Employees);
                             return Ok( employeeDto);
                }
                  [HttpGet("Company/{CompanyId}/GetEmployeeFilter")]

                public async Task< IActionResult> GetEmployeeFilter(Guid CompanyId,[FromQuery] EmployeePrameter employeePrameter)
                {
                    if(employeePrameter.IsIvalidAge)
                    {
                        return BadRequest("min Age Cant be more than Max age");
                    }
                    var Employees=await _mangeRepository.employeeRepository.GetEmployeesByCompanyFilter(CompanyId,employeePrameter,false);
                    Response.Headers.Add("Ex-Pagnation",JsonConvert.SerializeObject(Employees.metaData));
                    Console.WriteLine(JsonConvert.SerializeObject(Employees.metaData));
                             var employeeDto=_mapper.Map<IEnumerable< EmployeeDto>>(Employees);
                             return Ok( employeeDto);
                }

        
    }
}
