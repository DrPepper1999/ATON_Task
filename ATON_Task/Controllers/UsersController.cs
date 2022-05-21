using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.WebApi.Models;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetUserDetails;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.UpdateUser;
using Users.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Users.Application.Users.Commands.UpdateYourself;
using Users.Application.Users.Queries.GetUserYourself;
using Users.Domain;
using Users.Application.Users.Queries.GetUserListGreaterThen;
using Users.Application.Users.Queries.GetUserGreaterThen;
using Users.Application.Users.Commands.UpdateRecovery;

namespace Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Gets the list active(missing RevokedOn) of the users, list sorted by CreatedOn
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /Users
        /// </remarks>>
        /// <returns>Returns UserListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get the user by login
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /Users/Userlogin
        /// </remarks>>
        /// <param name="login">User login</param>
        /// <returns>Returns UserDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpGet("{login}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDetailsVm>> Get(string login)
        {
            var query = new GetUserDetailsQuery
            {
                Login = login
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get all users over a certain age
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /Users/greaterThen/login
        /// </remarks>>
        /// <param name="age">User age</param>
        /// <returns>Returns UserListGreaterThenVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpGet("greaterThen{age}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserListGreaterThenVm>> GetAllGreaterThan(int age)
        {
            var query = new GetUserListGreaterThenQuery
            {
                Age = age
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get information about yourself
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// GET /Users/yourself
        /// </remarks>>
        /// <returns>UserYourselfDataVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("yourself")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<User>> GetYourself()
        {
            var currentUserLogin = User.Identity.Name;
            var query = new GetUserYourselfQuery
            {
                Login = currentUserLogin
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the user
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// POST /Users
        /// {
        ///     login: "user login"
        ///     password: "user password"
        ///     name: "user name"
        ///     gender: "user gender"
        ///     birthDay: "user birth day"
        ///     admin: "is admin user"
        /// }
        /// </remarks>>
        /// <param name="CreateUserDto">CreateUserDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            var currentUserLogin = User.Identity.Name;
            command.CreatedBy = currentUserLogin;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        /// <summary>
        /// Update yourself
        /// </summary>
        /// <remarks>
        /// all fields are not required
        /// Simple request:
        /// PUT /Users
        /// {
        ///     login: "new user login"
        ///     password: "new user password"
        ///     name: "new user name"
        ///     gender: "new user gender"
        ///     birthDay: "new user birth day"
        /// }
        /// </remarks>>
        /// <param name="UpdateYourselfDto">UpdateYourselfDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateYourself([FromBody] UpdateYourselfDto updateYourselfDto)
        {
            var command = _mapper.Map<UpdateYourselfCommand>(updateYourselfDto);
            var currentUserLogin = User.Identity.Name;
            command.CurrentUserLogin = currentUserLogin;
            command.ModifiedBy = currentUserLogin;
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Update Users
        /// </summary>
        /// <remarks>
        /// all fields are not required
        /// Simple request:
        /// PUT /user/userLogin
        /// {
        ///     login: "new user login"
        ///     password: "new user password"
        ///     name: "new user name"
        ///     gender: "new user gender"
        ///     birthDay: "new user birth day"
        /// }
        /// </remarks>>
        /// <param name="login ,UpdateUserDto">user login ,UpdateUserDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpPut("{login}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(string login, [FromBody] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            var currentUser = User.Identity.Name;
            command.ModifiedBy = currentUser;
            command.UserLogin = login;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// User Recovery
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// PUT /Users/recovery/userLogin
        /// </remarks>>
        /// <param name="login">user login</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpPut("recovery{login}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateRecovery(string login)
        {
            var command = new UpdateRecoveryUserCommand
            {
                Login = login
            };
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>
        /// Simple request:
        /// DELETE /Users/userLogin?isFullDeleted=false
        /// </remarks>>
        /// <param name="isFullDeleted ,login">user login, full or soft removal</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user is not admin</response>
        [HttpDelete("{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string login, bool isFullDeleted = false)
        {
            var currentUser = User.Identity.Name;
            var command = new DeleteUserCommand
            {
                Login = login,
                IsFullDeleted = isFullDeleted,
                RevokedBy = currentUser
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
