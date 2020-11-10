/*
 * Copyright (c) 2020 ZP
 * Revision: 0.0.0.1
 * CLR: 4.0.30319.42000
 * Date 8/11/2020 9:50:16 AM
 * Name DTOWXTest
 * Create on device ZPX.ZPX
 * Author Create By ZHAOPAN
 * Describe something
 *
 */

namespace dotnet.comlib.test
{
    public class DTOWXTest : DTOShopDiyBase
    {
        public string NameEx { get; set; }
        public string CodeEx { get; set; }
    }

    /// <summary>
    /// 基础类
    /// </summary>
    public abstract class DTOShopDiyBase
    {
        /// <summary>
        /// Id标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据属性
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// <see cref="EShopDiyDocumentType"/>
        /// </summary>
        public int DataType { get; set; }
    }
}