/*
 * ========================================================================
 * Copyright(c) 2006-2010 PWMIS, All Rights Reserved.
 * Welcom use the PDF.NET (PWMIS Data Process Framework).
 * See more information,Please goto http://www.pwmis.com/sqlmap 
 * ========================================================================
 * ���������
 * 
 * ���ߣ���̫��     ʱ�䣺2008-10-12
 * �汾��V3.0
 * 
 * �޸��ߣ�         ʱ�䣺                
 * �޸�˵����
 * ========================================================================
*/
using System;
using System.Data;

namespace PWMIS.Common
{
	/// <summary>
	/// ����ӳ��ؼ��ӿ�
	/// </summary>
	public interface IDataControl
	{
		
		/// <summary>
		/// �����ݿ������������������
		/// </summary>
		string LinkProperty
		{
			get;
			set;
		}
		
		/// <summary>
		/// �����ݹ����ı���
		/// </summary>
		string LinkObject
		{
			get;
			set;
		}

		/// <summary>
		/// �Ƿ�ͨ����������֤Ĭ��Ϊtrue
		/// </summary>
		bool IsValid
		{
			get;
		}

//		/// <summary>
//		/// ��������
//		/// </summary>
//		DbType DataType
//		{
//			get;
//			set;
//		}

		/// <summary>
		/// ��������
		/// </summary>
		TypeCode SysTypeCode
		{
			get;
			set;
		}

		/// <summary>
		/// ֻ�����
		/// </summary>
		bool ReadOnly
		{
			get;
			set;
		}

		/// <summary>
		/// �Ƿ�ͻ�����֤
		/// </summary>
//		bool isClientValidation
//		{
//			get;
//			set;
//		}

		/// <summary>
		/// �Ƿ������ֵ
		/// </summary>
		bool IsNull
		{
			get;
//			set;
		}

		/// <summary>
		/// �Ƿ�������
		/// </summary>
		bool PrimaryKey
		{
			get;
			set;
        }

//		object 

//		/// <summary>
//		/// �ͻ�����֤�ű�
//		/// </summary>
//		string ClientValidationFunctionString
//		{
//			get;
//			set;
//		}

		/// <summary>
		/// ����ֵ
		/// </summary>
		/// <param name="obj"></param>
		void SetValue(object value);

		/// <summary>
		/// ��ȡֵ
		/// </summary>
		/// <returns></returns>
		object GetValue();

		/// <summary>
		/// �������֤
		/// </summary>
		/// <returns></returns>
		bool Validate();
        ///// <summary>
        ///// �趨��Ӧ������Դ����ʽ��FullClassName,AssemblyName �������Ҫ��ʵ���࣬�������ø����ԡ�
        ///// </summary>
        //string DataProvider { get; set; }

	}

    public interface IDataTextBox : IDataControl
    {
        string Text { get; set; }
        string DataFormatString { get; set; }
        int MaxLength { get; set; }
    }

    public interface IDataCheckBox : IDataControl
    {
        string Value { get; set; }
        bool Checked { get; set; }
        string Text { get; set; }
        event EventHandler CheckedChanged;
    }

    
}
