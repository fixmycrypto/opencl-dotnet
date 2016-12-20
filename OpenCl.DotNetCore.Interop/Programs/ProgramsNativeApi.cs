
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Programs
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Programs API.
    /// </summary>
    public static class ProgramsNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Creates a program object for a context, and loads the source code specified by the text strings in the <see cref="strings"/> array into the program object.
        /// </summary>
        /// <param name="context">Must be a valid OpenCL context.</param>
        /// <param name="count">The number of source code strings that are provided.</param>
        /// <param name="strings">An array of <see cref="count"/> pointers to optionally null-terminated character strings that make up the source code.</param>
        /// <param name="lengths">
        /// An array with the number of chars in each string (the string length). If an element in <see cref="lengths"/> is zero, its accompanying string is null-terminated. If lengths is <c>null</c>, all strings in the strings argument are
        /// considered null-terminated. Any length value passed in that is greater than zero excludes the null terminator in its count.
        /// </param>
        /// <param name="errorCode">Returns an appropriate error code. If errorCode is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero program object and <see cref="errorCode"/> is set to <c>Result.Success</c> if the program object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errorCode"/>:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid context.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="count"/> is zero or if strings or any entry in strings is <c>null</c>.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateProgramWithSource")]
        public static extern IntPtr CreateProgramWithSource(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U4)] uint count,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] strings,
            [In] [MarshalAs(UnmanagedType.LPArray)] uint[] lengths,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        //extern CL_API_ENTRY cl_program CL_API_CALL
        //clCreateProgramWithBinary(cl_context                     /* context */,
        //                        cl_uint                        /* num_devices */,
        //                        const cl_device_id *           /* device_list */,
        //                        const size_t *                 /* lengths */,
        //                        const unsigned char **         /* binaries */,
        //                        cl_int *                       /* binary_status */,
        //                        cl_int *                       /* errcode_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_program CL_API_CALL
        //clCreateProgramWithBuiltInKernels(cl_context            /* context */,
        //                                cl_uint               /* num_devices */,
        //                                const cl_device_id *  /* device_list */,
        //                                const char *          /* kernel_names */,
        //                                cl_int *              /* errcode_ret */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_program CL_API_CALL
        //clCreateProgramWithIL(cl_context    /* context */,
        //                    const void*    /* il */,
        //                    size_t         /* length */,
        //                    cl_int*        /* errcode_ret */) CL_API_SUFFIX__VERSION_2_1;


        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainProgram(cl_program /* program */) CL_API_SUFFIX__VERSION_1_0;

        /// <summary>
        /// Decrements the program reference count.
        /// </summary>
        /// <param name="program">The program to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseProgram")]
        public static extern Result ReleaseProgram([In] IntPtr program);

        /// <summary>
        /// Builds (compiles and links) a program executable from the program source or binary.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="numberOfDevices">The number of devices listed in <see cref="deviceList"/>.</param>
        /// <param name="deviceList">
        /// A pointer to a list of devices associated with <see cref="program"/>. If <see cref="deviceList"/> is a <c>null</c> value, the program executable is built for all devices associated with <see cref="program"/> for which a source
        /// or binary has been loaded. If <see cref="deviceList"/> is a non-<c>null</c> value, the program executable is built for devices specified in this list for which a source or binary has been loaded.
        /// </param>
        /// <param name="options">A pointer to a null-terminated string of characters that describes the build options to be used for building the program executable. Certain options are ignored when program is created with IL.</param>
        /// <param name="notificationCallback">
        /// A function pointer to a notification routine. The notification routine is a callback function that an application can register and which will be called when the program executable has been built (successfully or unsuccessfully).
        /// If <see cref="notificationCallback"/> is not <c>null</c>, <see cref="BuildProgram"/> does not need to wait for the build to complete and can return immediately once the build operation can begin. The build operation can begin if
        /// the context, program whose sources are being compiled and linked, list of devices and build options specified are all valid and appropriate host and device resources needed to perform the build are available. If
        /// <see cref="notificationCallback"/> is <c>null</c>, <see cref="BuildProgram"/> does not return until the build has completed. This callback function may be called asynchronously by the OpenCL implementation. It is the
        /// application’s responsibility to ensure that the callback function is thread-safe.
        /// </param>
        /// <param name="userData">Passed as an argument when <see cref="notificationCallback"/> is called. <see cref="userData"/> can be <c>null</c>.</param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clBuildProgram")]
        public static extern Result BuildProgram(
            [In] IntPtr program,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfDevices,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] deviceList,
            [In] [MarshalAs(UnmanagedType.LPStr)] string options,
            [In] IntPtr notificationCallback,
            [In] IntPtr userData
        );

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clCompileProgram(cl_program           /* program */,
        //                cl_uint              /* num_devices */,
        //                const cl_device_id * /* device_list */,
        //                const char *         /* options */, 
        //                cl_uint              /* num_input_headers */,
        //                const cl_program *   /* input_headers */,
        //                const char **        /* header_include_names */,
        //                void (CL_CALLBACK *  /* pfn_notify */)(cl_program /* program */, void * /* user_data */),
        //                void *               /* user_data */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_program CL_API_CALL
        //clLinkProgram(cl_context           /* context */,
        //            cl_uint              /* num_devices */,
        //            const cl_device_id * /* device_list */,
        //            const char *         /* options */, 
        //            cl_uint              /* num_input_programs */,
        //            const cl_program *   /* input_programs */,
        //            void (CL_CALLBACK *  /* pfn_notify */)(cl_program /* program */, void * /* user_data */),
        //            void *               /* user_data */,
        //            cl_int *             /* errcode_ret */ ) CL_API_SUFFIX__VERSION_1_2;


        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clUnloadPlatformCompiler(cl_platform_id /* platform */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetProgramInfo(cl_program         /* program */,
        //                cl_program_info    /* param_name */,
        //                size_t             /* param_value_size */,
        //                void *             /* param_value */,
        //                size_t *           /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        /// <summary>
        /// Returns build information for each device in the program object.
        /// </summary>
        /// <param name="program">Specifies the program object being queried.</param>
        /// <param name="device">Specifies the device for which build information is being queried. <see cref="device"/> must be a valid device associated with <see cref="program"/>.</param>
        /// <param name="parameterName">Specifies the information to query.</param>
        /// <param name="parameterValueSize">Used to specify the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size must be greater or equal to the size of the return type.</param>
        /// <param name="parameterValue">A pointer to memory where the appropriate result being queried is returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">The actual size in bytes of data copied to <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not a valid device object.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/>
        /// is not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetProgramBuildInfo")]
        public static extern Result GetProgramBuildInformation(
            [In] IntPtr program,
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] ProgramBuildInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        #endregion

        #region Deprecated Public Methods

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clUnloadCompiler(void) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        #endregion
    }
}