﻿
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL library.
    /// </summary>
    public static class NativeMethods
    {
        #region Platform API Methods

        /// <summary>
        /// Obtain the list of platforms available.
        /// </summary>
        /// <param name="num_entries">
        /// The number of platform entries that can be added to <see cref="platforms"/>. If <see cref="platforms"/> is not
        /// <c>null</c>, the <see cref="num_entries"/> must be greater than zero.
        /// </param>
        /// <param name="platforms">
        /// Returns a list of OpenCL platforms found. The platform values returned in <see cref="platforms"/> can be used to
        /// identify a specific OpenCL platform. If <see cref="platforms"/> argument is <c>null</c>, this argument is ignored. The
        /// number of OpenCL platforms returned is the mininum of the value specified by <see cref="num_entries"/> or the number
        /// of OpenCL platforms available.
        /// </param>
        /// <param name="num_platforms">
        /// Returns the number of OpenCL platforms available. If <see cref="num_platforms"/> is <c>null</c>, this argument is
        /// ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. If the cl_khr_icd extension is enabled, 
        /// <see cref="GetPlatformIds"/> returns <c>Result.Success</c> if the function is executed successfully and there are a non
        /// zero number of platforms available. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="platforms"/> is not <c>null</c>,
        /// or if both <see cref="num_platforms"/> and <see cref="platforms"/> are <c>null</c>.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// 
        /// <c>Result.PlatformNotFoundKhr</c> if the cl_khr_icd extension is enabled and no platforms are found.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformIDs")]
        public static extern Result GetPlatformIds(
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_platforms
        );

        /// <summary>
        /// Get specific information about the OpenCL platform.
        /// </summary>
        /// <param name="platform">
        /// The platform ID returned by <see cref="GetPlatformIds"/> or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>,
        /// the behavior is implementation-defined.
        /// </param>
        /// <param name="param_name">An enumeration constant that identifies the platform information being queried.</param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than
        /// or equal to size of return type specified above.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_value"/> will be returned. If
        /// <see cref="param_value"/> is <c>null</c>, it is ignored.
        /// </param>
        /// <param name="param_value_size_ret">
        /// Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following: (The OpenCL
        /// specification does not describe the order of precedence for error codes returned by API calls)
        /// 
        /// <c>Result.InvalidPlatform</c> if platform is not a valid platform.!--
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified
        /// by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/ is not a <c>null</c>
        /// value.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformInfo")]
        public static extern Result GetPlatformInfo(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] PlatformInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret
        );

        #endregion

        #region Device API Methods

        /// <summary>
        /// Obtain the list of devices available on a platform.
        /// </summary>
        /// <param name="platform">
        /// Refers to the platform ID returned by <see cref="GetPlatformIds"/< or can be <c>null</c>. If <see cref="platform"/> is
        /// <c>null</c>, the behavior is implementation-defined.
        /// </param>
        /// <param name="device_type">
        /// A bitfield that identifies the type of OpenCL device. The <see cref="device_type"/> can be used to query specific OpenCL
        /// devices or all OpenCL devices available.
        /// </param>
        /// <param name="num_entries">
        /// The number of device entries that can be added to <see cref="devices"/>. If <see cref="devices"/> is not <c>null</c>,
        /// the <see cref="num_entries"/> must be greater than zero.
        /// </param>
        /// <param name="devices">
        /// A list of OpenCL devices found. The device values returned in <see cref="devices"/> can be used to identify a specific
        /// OpenCL device. If <see cref="devices"/> argument is <c>null</c>, this argument is ignored. The number of OpenCL devices
        /// returned is the mininum of the value specified by <see cref="num_entries"/> or the number of OpenCL devices whose type
        /// matches <see cref="device_type"/>.
        /// </param>
        /// <param name="num_devices">
        /// The number of OpenCL devices available that match <see cref="device_type". If <see cref="num_devices"/> is <c>null</c>,
        /// this argument is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidPlatform</c> if <see cref="platform"/> is not a valid platform.
        /// 
        /// <c>Result.InvalidDeviceType</c> if <see cref="device_type"/> is not a valid value.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="devices"/> is not <c>null</c>
        /// or if both <see cref="num_devices"/> and <see cref="devices"/> are <c>null</c>.
        /// 
        /// <c>Result.DeviceNotFound</c> if no OpenCL devices that matched <see cref="device_type"/> were found.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceIDs")]
        public static extern Result GetDeviceIds(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceType device_type,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_devices
        );

        /// <summary>
        /// Get information about an OpenCL device.
        /// </summary>
        /// <param name="device">
        /// A device returned by <see cref="GetDeviceIds"/>. May be a device returned by <see cref="GetDeviceIds"/> or a sub-device
        /// created by <see cref="CreateSubDevices"/>. If device is a sub-device, the specific information for the sub-device will
        /// be returned.
        /// </param>
        /// <param name="param_name">An enumeration constant that identifies the device information being queried.</param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than
        /// or equal to the size of return type specified.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_name"/>. If <see cref="param_value"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <param name="param_value_size_ret">
        /// Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not valid.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified
        /// by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/> is not a <c>null</c>
        /// value or if <see cref="param_name"/> is a value that is available as an extension and the corresponding extension is
        /// not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceInfo")]
        public static extern Result GetDeviceInfo(
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret
        );

        #endregion

        #region Context API Methods

        /// <summary>
        /// Creates an OpenCL context.
        /// </summary>
        /// <param name="properties">
        /// Specifies a list of context property names and their corresponding values. Each property name is immediately followed
        /// by the corresponding desired value. The list is terminated with 0. <see cref="properties"/> can be <c>null</c> in which
        /// case the platform that is selected is implementation-defined.
        /// </param>
        /// <param name="num_devices">The number of devices specified in the <see cref="devices"/> argument.</param>
        /// <param name="devices">
        /// A pointer to a list of unique devices returned by <see cref="GetDeviceIds"/> or sub-devices created by
        /// <see cref="CreateSubDevices"/> for a platform. Duplicate devices specified in <see cref="devices"/> are ignored.
        /// </param>
        /// <param name="pfn_notify">
        /// A callback function that can be registered by the application. This callback function will be used by the OpenCL
        /// implementation to report information on errors during context creation as well as errors that occur at runtime in this
        /// context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's
        /// responsibility to ensure that the callback function is thread-safe. If <see cref="pfn_notify"/> is <c>null</c>, no
        /// callback function is registered. The parameters to this callback function are:
        /// 
        /// errinfo is a pointer to an error string.
        /// 
        /// private_info and cb represent a pointer to binary data that is returned by the OpenCL implementation that can be used
        /// to log additional information helpful in debugging the error.
        /// 
        /// user_data is a pointer to user supplied data.
        /// 
        /// Note: There are a number of cases where error notifications need to be delivered due to an error that occurs outside a
        /// context. Such notifications may not be delivered through the <see cref="pfn_notify"/> callback. Where these
        /// notifications go is implementation-defined.
        /// </param>
        /// <param name="user_data">
        /// Passed as the user_data argument when <see cref="pfn_notify"/> is called. <see cref="user_data"/> can be <c>null</c>.
        /// </param>
        /// <param name="errcode_ret">
        /// Returns an appropriate error code. If <see cref="errcode_ret"/> is <c>null</c>, no error code is returned.
        /// </param>
        /// <returns>
        /// Returns a valid non-zero context and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the context is created
        /// successfully. Otherwise, it returns a <c>null</c> value with an error value returned in <see cref="errcode_ret"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateContext")]
        public static extern IntPtr CreateContext(
            [In] IntPtr[] properties,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_devices,
            [In] IntPtr[] devices,
            [In] IntPtr pfn_notify,
            [In] IntPtr user_data,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
        );

        #endregion

        #region Program Object API Methods

        /// <summary>
        /// Creates a program object for a context, and loads the source code specified by the text strings in the
        /// <see cref="strings"/> array into the program object.
        /// </summary>
        /// <param name="context">Must be a valid OpenCL context.</param>
        /// <param name="count">The number of source code strings that are provided.</param>
        /// <param name="strings">
        /// An array of <see cref="count"/> pointers to optionally null-terminated character strings that make up the source code.
        /// </param>
        /// <param name="lengths">
        /// An array with the number of chars in each string (the string length). If an element in <see cref="lengths"/> is zero,
        /// its accompanying string is null-terminated. If lengths is <c>null</c>, all strings in the strings argument are
        /// considered null-terminated. Any length value passed in that is greater than zero excludes the null terminator in its
        /// count.
        /// </param>
        /// <param name="errcode_ret">
        /// Returns an appropriate error code. If errcode_ret is <c>null</c>, no error code is returned.
        /// </param>
        /// <returns>
        /// Returns a valid non-zero program object and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the program
        /// object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errcode_ret"/>:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid context.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="count"/> is zero or if strings or any entry in strings is <c>null</c>.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateProgramWithSource")]
        public static extern IntPtr CreateProgramWithSource(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U4)] uint count,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] strings,
            [In] [MarshalAs(UnmanagedType.LPArray)] uint[] lengths,
            [Out] out Result errcode_ret
        );

        /// <summary>
        /// Decrement the context reference count.
        /// </summary>
        /// <param name="context">The context to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following
        /// errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid OpenCL context.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseContext")]
        public static extern Result ReleaseContext(IntPtr context);

        #endregion
    }
}